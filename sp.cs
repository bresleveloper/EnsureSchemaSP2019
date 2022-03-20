using Microsoft.SharePoint;
using System;

public class SP : IDisposable
{
    public SPSite Site { get; set; }
    public SP(string webUrl)
	{
        using (SPSite site = new SPSite(webUrl))
        {
            Site = site;
            Site.RootWeb.AllowUnsafeUpdates = true;
        }
    }

    public void Dispose()
    {
        Site.Close();
        Site.Dispose();
    }

    public bool IsSiteField(string name)
    {
        if (Site.RootWeb.Fields.ContainsField(name) == true ||
            Site.RootWeb.Fields.ContainsFieldWithStaticName(name) == true)
        {
            return true;
        }
        return false;
    }

    public SPField CreateFieldInSite(SchemaField data)
    {

        Site.RootWeb.Fields.Add(data.Name, data.Type, data.Required);
        //SPField f = new SPField(Site.RootWeb.Fields, data.Name, data.DisplayName);
        //Site.RootWeb.Update();
        SPField f = Site.RootWeb.Fields.GetField(data.Name);
        
        f.Group = "ensure-v4";
        f.Title = data.DisplayName;

        f.Update();
        return f;
    }

    public bool IsCT_inSite(string ctname)
    {
        SPContentType ct = Site.RootWeb.ContentTypes[ctname];
        return ct != null;
    }

    public SPContentType CreateCTinSite(SchemaCT data)
    {
        SPContentTypeId parent_ctId = new SPContentTypeId(data.ParentCTID);
        SPContentType parentCT = Site.RootWeb.ContentTypes[parent_ctId];
        SPContentTypeId ctId = new SPContentTypeId(data.CTID);
        SPContentType ct = new SPContentType(ctId, Site.RootWeb.ContentTypes, data.Name);

        ct.Group = "ensure-v4";
        Console.WriteLine("Site.RootWeb.ContentTypes.Add(ct);");
        Site.RootWeb.ContentTypes.Add(ct);
        Console.WriteLine("Site.RootWeb.Update();");
        Site.RootWeb.Update();

        data.myFields.ForEach(fname =>
        {
            Console.WriteLine("add field " + fname);
            try
            {
                var spf = Site.RootWeb.Fields.GetField(fname);
                var spfl = new SPFieldLink(spf);
                ct.FieldLinks.Add(spfl);
            }
            catch (Exception ex)
            {
                Console.WriteLine("problem adding field " + fname);
            }
        });

        Console.WriteLine("ct.Update();");
        ct.Update();

        return ct;
    }

    public bool IsList_inSite(string listname)
    {
        SPList splist = Site.RootWeb.Lists.TryGetList(listname);
        return splist != null;
    }

    public SPList CreateList_inSite(SchemaList data)
    {
        Guid listguid = Site.RootWeb.Lists.Add(data.Name, string.Empty, SPListTemplateType.GenericList);
        SPList splist = Site.RootWeb.Lists.GetList(listguid, true);

        data.myFields.ForEach(fname =>
        {
            Console.WriteLine("add field " + fname);
            try
            {
                var spf = Site.RootWeb.Fields.GetField(fname);
                splist.Fields.Add(spf);
            }
            catch (Exception ex)
            {
                Console.WriteLine("problem adding field " + fname);
            }
        });

        Console.WriteLine("splist.Update();");
        splist.Update();

        Console.WriteLine("splist add fields to view");
        SPView view = splist.DefaultView;

        data.myFields.ForEach(fname => view.ViewFields.Add(fname));
        Console.WriteLine("splist.DefaultView.Update();");
        view.Update();

        return splist;
    }
}
