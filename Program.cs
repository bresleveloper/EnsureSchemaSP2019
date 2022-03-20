using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ensure_v4
{
    internal class Program
    {
        static ConsoleColor defaultColor = Console.ForegroundColor;

        static Action<string> l = (m) =>
        {
            string filename = "log_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + ".txt";
            bool logToFile = true;
            try
            {
                File.AppendAllText(filename, "LOG FOR " + DateTime.Now.ToString("f"));
            }
            catch (Exception ex)
            {
                logToFile = false;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cant create log file, " + ex.ToString());
                Console.ForegroundColor = defaultColor;
            }

            try
            {
                if (logToFile == true)
                {
                    File.AppendAllText(filename, m + "\r\n");
                }
                Console.WriteLine(m);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cant write to log file, " + ex.ToString());
                Console.ForegroundColor = defaultColor;
            }
        };

        static void Main(string[] args)
        {
            l("Hello SP-MAN!");

            l("attempting to get site from url : " + args[0]);
            SP sp = new SP(args[0]);
            l("Got site " + sp.Site.Url);
            //SchemaBase schema = new Portal();
            SchemaBase schema = new NAVI();

            l("1st test takes a few..." );

            //debug
            /*if (false)
            {
                SPList splist = sp.Site.RootWeb.Lists.TryGetList("TopNavIconsLinks");
                Console.WriteLine("splist add fields to view");
                SPView view = splist.DefaultView;
                schema.Lists[0].myFields.ForEach(fname => view.ViewFields.Add(fname));
                Console.WriteLine("splist.DefaultView.Update();");
                view.Update();
                return;
            }*/

            if (schema.Fields != null)
            {
                l("\nTest for site fields\n");
                schema.Fields.ForEach(f =>
                {
                    if (sp.IsSiteField(f.Name) == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        l(f.Name + " Exists");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        l(f.Name + " NOT Exists");

                        Console.ForegroundColor = defaultColor;
                        l("Create Field" + f.Name);
                        var spf = sp.CreateFieldInSite(f);
                        if (f.Name == "typeOfUpdate")
                        {
                            SPFieldChoice colChoice = (SPFieldChoice)spf;
                            colChoice.FillInChoice = true;
                            colChoice.Update();
                        }
                        if (f.Name == "isActive")
                        {
                            spf.DefaultValue = "1";
                            spf.Update();
                        }

                    }
                });
                Console.ForegroundColor = defaultColor;
            }

            if (schema.ContentTypes != null)
            {
                l("\n\nTest for site content types\n");
                schema.ContentTypes.ForEach(ct =>
                {
                    if (sp.IsCT_inSite(ct.Name) == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        l(ct.Name + " Exists");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        l(ct.Name + " NOT Exists");

                        Console.ForegroundColor = defaultColor;
                        l("Create content type " + ct.Name);
                        var spct = sp.CreateCTinSite(ct);
                    }
                });
                Console.ForegroundColor = defaultColor;

            }

            if (schema.Lists != null)
            {
                l("\n\nTest for site lists\n");
                schema.Lists.ForEach(list =>
                {
                    if (sp.IsList_inSite(list.Name) == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        l(list.Name + " Exists");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        l(list.Name + " NOT Exists");

                        Console.ForegroundColor = defaultColor;
                        l("Create list " + list.Name);
                        var splist = sp.CreateList_inSite(list);

                        if (list.Name == "TopNavIconsLinks")
                        {
                            SPListItem searchItem = splist.Items.Add();
                            searchItem["Title"] = "search";
                            SPFieldUrlValue searchLink = new SPFieldUrlValue();
                            searchLink.Url = "https://www.google.com/search?q={q}";
                        //searchLink.Description = "";
                        searchItem["TopNavIcon_LinkUrl"] = searchLink;
                            searchItem.Update();
                        }

                    }
                });
                Console.ForegroundColor = defaultColor;
            }


            Console.ForegroundColor = defaultColor;
            l("\n\n\nPRESS ANY KEY");
            Console.ReadKey();
        }

    }
}
