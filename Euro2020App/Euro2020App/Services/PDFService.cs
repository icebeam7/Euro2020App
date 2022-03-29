/*using System;
using System.Collections.Generic;
using System.Text;

using Euro2020App.Interfaces;
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

namespace Euro2020App.Services
{
    public class PDFService
    {

        public async static Task Borders()
        {
            GlobalFontSettings.FontResolver = new FontResolver();

            Document document = new Document();

            Section sec = document.Sections.AddSection();
            sec.AddParagraph("A paragraph before.");
            Table table = sec.AddTable();
            table.Borders.Visible = true;
            table.AddColumn();
            table.AddColumn();
            table.Rows.HeightRule = RowHeightRule.Exactly;
            table.Rows.Height = 14;
            Row row = table.AddRow();
            Cell cell = row.Cells[0];
            cell.Borders.Visible = true;
            cell.Borders.Left.Width = 8;
            cell.Borders.Right.Width = 2;
            cell.AddParagraph("First Cell");

            row = table.AddRow();
            cell = row.Cells[1];
            cell.AddParagraph("Last Cell within this table");
            cell.Borders.Bottom.Width = 15;
            cell.Shading.Color = Colors.LightBlue;
            sec.AddParagraph("A Paragraph afterwards");

            var ic = DependencyService.Get<ILocalPath>();
            var localPath = await ic.GuardarImagen();

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

        public async static Task CellMerge()
        {
            Document document = new Document();
            Section sec = document.Sections.AddSection();
            sec.AddParagraph("A paragraph before.");
            Table table = sec.AddTable();
            table.Borders.Visible = true;
            table.AddColumn();
            table.AddColumn();
            Row row = table.AddRow();
            Cell cell = row.Cells[0];
            cell.MergeRight = 1;
            cell.Borders.Visible = true;
            cell.Borders.Left.Width = 8;
            cell.Borders.Right.Width = 2;
            cell.AddParagraph("First Cell");

            row = table.AddRow();
            cell = row.Cells[1];
            cell.AddParagraph("Last Cell within this row");
            cell.MergeDown = 1;
            cell.Borders.Bottom.Width = 15;
            cell.Borders.Right.Width = 30;
            cell.Shading.Color = Colors.LightBlue;
            row = table.AddRow();
            sec.AddParagraph("A Paragraph afterwards");

            var ic = DependencyService.Get<ILocalPath>();
            var localPath = await ic.GuardarImagen();

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


        public async static Task VerticalAlign()
        {
            Document document = new Document();
            Section sec = document.Sections.AddSection();
            sec.AddParagraph("A paragraph before.");
            Table table = sec.AddTable();
            table.Borders.Visible = true;
            table.AddColumn();
            table.AddColumn();
            Row row = table.AddRow();
            row.HeightRule = RowHeightRule.Exactly;
            row.Height = 70;
            row.VerticalAlignment = VerticalAlignment.Center;
            row[0].AddParagraph("First Cell");
            row[1].AddParagraph("Second Cell");
            sec.AddParagraph("A Paragraph afterwards.");


            var ic = DependencyService.Get<ILocalPath>();
            var localPath = await ic.GuardarImagen();

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
    }

    //This implementation is obviously not very good --> Though it should be enough for everyone to implement their own.
    public class FontResolver : IFontResolver
    {
        public string DefaultFontName => "verdana";

        public byte[] GetFont(string faceName)
        {
            if (DefaultFontName.Contains(faceName))
            {
                var assembly = typeof(PDFService).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream($"PDFTest2.Fonts.{faceName}.ttf");
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
*/