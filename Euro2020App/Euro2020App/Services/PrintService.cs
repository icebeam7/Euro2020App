using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Tables;
using MigraDocCore.DocumentObjectModel.IO;
using Cell = MigraDocCore.DocumentObjectModel.Tables.Cell;
using MigraDocCore.Rendering;
using PdfSharpCore.Fonts;
using PdfSharpCore.Utils;
using System.IO;
using System.Reflection;
using Euro2020App.Models;
using Euro2020App.Interfaces;
using PdfSharpCore.Drawing;
using System.Net.Http;

namespace Euro2020App.Services
{
    public class PrintService
    {
        public async static Task CreatePdf(EuroGroup group, List<EuroTeam> teams)
        {
            GlobalFontSettings.FontResolver = new FontResolver();
            var ic = DependencyService.Get<ILocalPath>();

            Document document = new Document();

            Section sec = document.Sections.AddSection();
            sec.AddParagraph($"Euro 2020 Details - {group.Name}");

            Table table = sec.AddTable();
            table.Borders.Visible = true;

            for (int i = 0; i < 5; i++)
            {
                table.AddColumn();
            }

            table.Rows.HeightRule = RowHeightRule.Exactly;
            table.Rows.Height = 30;

            Row headerRow = table.AddRow();

            AddCell(headerRow, 0, "Flag", true);
            AddCell(headerRow, 1, "Team", true);
            AddCell(headerRow, 2, "Games", true);
            AddCell(headerRow, 3, "Points", true);
            AddCell(headerRow, 4, "Goal Difference", true);

            var client = new HttpClient();

            foreach (var team in teams)
            {
                Row row = table.AddRow();

                var getImage = await client.GetAsync(team.FK_Country.FlagUrl);

                if (getImage.IsSuccessStatusCode)
                {
                    var stream = await getImage.Content.ReadAsStreamAsync();
                    stream.Position = 0;

                    //var localImage = await ic.SaveImage();
                    //var image = MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource.FromStream(localImage, () => stream);
                    //cell0.AddImage(image).Width = "3cm";
                }

                AddCell(row, 1, team.FK_Country.Name);
                AddCell(row, 2, team.Games.ToString());
                AddCell(row, 3, team.Points.ToString());
                AddCell(row, 4, team.GoalDifference.ToString());
            }


            var localPath = await ic.GetPath();

            PdfDocumentRenderer printer = new PdfDocumentRenderer()
            {
                Document = document
            };
            printer.RenderDocument();
            printer.PdfDocument.Save(localPath);

            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(localPath)
            });

        }

        private static void AddCell(Row row, int index, string text, bool isHeader = false)
        {
            Cell cell = row.Cells[index];
            cell.Borders.Visible = true;
            cell.Format.Font.Bold = isHeader;
            cell.Format.Alignment = isHeader ? ParagraphAlignment.Center : ParagraphAlignment.Left;
            cell.Format.Borders.Left = new Border() { Width = 8 };
            cell.AddParagraph(text);
        }
    }

    public class FontResolver : IFontResolver
    {
        public string DefaultFontName => "verdana";

        public byte[] GetFont(string faceName)
        {
            if (DefaultFontName.Contains(faceName))
            {
                var assembly = typeof(PrintService).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream($"Euro2020App.Fonts.{faceName}.ttf");
                using (var reader = new StreamReader(stream))
                {
                    var bytes = default(byte[]);
                    using (var memstream = new MemoryStream())
                    {
                        reader.BaseStream.CopyTo(memstream);
                        bytes = memstream.ToArray();
                    }
                    return bytes;
                }
            }
            else
            {
                return GetFont(DefaultFontName);
            }
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("Verdana", StringComparison.CurrentCultureIgnoreCase))
            {
                if (isBold && isItalic)
                {
                    return new FontResolverInfo("Verdana Bold");
                }
                else if (isBold)
                {
                    return new FontResolverInfo("verdanab");
                }
                else if (isItalic)
                {
                    return new FontResolverInfo("VERDANA0");
                }
                else
                {
                    return new FontResolverInfo("verdana");
                }
            }
            return null;
        }
    }
}
