using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECIB.App_Code
{
    public class GridViewExportUtil
    {
        public static void Export(string fileName, GridView gv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid
                    
                    Table table = new Table();
                    table.GridLines = gv.GridLines;
                    table.HorizontalAlign = HorizontalAlign.Left;
                    table.BorderWidth = 1;
                    table.BorderColor = System.Drawing.Color.FromName("#247BD2");
                    table.Font.Size = 9;
                    table.Width = 90;
                    //  add the header row to the table
                    if (gv.HeaderRow != null)
                    {
                        for (int i = 0; i <= gv.HeaderRow.Cells.Count - 1; i++)
                        {
                            gv.HeaderRow.Cells[i].Width = 90;
                            gv.HeaderRow.Font.Size = 9;
                            gv.HeaderRow.Cells[i].BackColor = System.Drawing.Color.FromName("#247BD2");
                            gv.HeaderRow.Cells[i].ForeColor = System.Drawing.Color.White;
                            gv.HeaderRow.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                        }
                        GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table


                    foreach (GridViewRow row in gv.Rows)
                    {
                        GridViewExportUtil.PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }


        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i <= control.Controls.Count - 1; i++)
            {
                Control current = control.Controls[i];

                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    GridViewExportUtil.PrepareControlForExport(current);
                }
            }
        }
    }
}