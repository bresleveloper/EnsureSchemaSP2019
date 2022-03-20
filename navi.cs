using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

public class NAVI : SchemaBase
{
    public NAVI()
    {
        Fields = new List<SchemaField>()
        {
            //new SchemaField(){ Name="Value", DisplayName="Value", Type=SPFieldType.Text },

            new SchemaField(){ Name="Order", DisplayName="Order", Type=SPFieldType.Number },
            new SchemaField(){ Name="isActive", DisplayName="is Active", Type=SPFieldType.Boolean },

            new SchemaField(){ Name="TopNavIcon_LinkIcon", DisplayName="TopNavIcon_LinkIcon", Type=SPFieldType.URL},
            new SchemaField(){ Name="TopNavIcon_LinkUrl", DisplayName="TopNavIcon_LinkUrl", Type=SPFieldType.URL},
            new SchemaField(){ Name="TopNavIconHover", DisplayName="TopNavIconHover", Type=SPFieldType.URL},
        };

        Lists = new List<SchemaList>()
        {
            new SchemaList()
            {
                Name="TopNavIconsLinks", 
                myFields = new List<string>()
                { 
                    "TopNavIcon_LinkIcon", "TopNavIcon_LinkUrl", "TopNavIconHover", "Order", "isActive"
                },
                
            }
        };
    }


}


