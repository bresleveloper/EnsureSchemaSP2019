using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

public abstract class SchemaBase
{
    public List<SchemaField> Fields { get; set; }
    public List<SchemaCT> ContentTypes { get; set; }
    public List<SchemaList> Lists { get; set; }

}

public class SchemaField
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public SPFieldType Type { get; set; }
    public bool Required { get; set; }
}

public class SchemaCT
{
    public string CTID { get; set; }
    public string ParentCTID { get; set; }
    public string Name { get; set; }
    public List<string> myFields { get; set; }
}

public class SchemaList
{ 
    public string Name { get; set; }
    public List<string> myFields { get; set; }
}


