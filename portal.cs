using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

public class Portal : SchemaBase
{
    public Portal()
    {
        Fields = new List<SchemaField>()
        {
            new SchemaField(){ Name="Order", DisplayName="Order", Type=SPFieldType.Number },
            new SchemaField(){ Name="isActive", DisplayName="is Active", Type=SPFieldType.Boolean },

            //new SchemaField(){ Name="newLink", DisplayName="new Link", Type="URL" },
            new SchemaField(){ Name="typeOfUpdate", DisplayName="type Of Update", Type=SPFieldType.Choice, Required=true },
            //new SchemaField(){ Name="title_x0020_bold_x0020_text", DisplayName="title bold text", Type=SPFieldType.Text },
            new SchemaField(){ Name="titleBoldText", DisplayName="titleBoldText", Type=SPFieldType.Text },

            new SchemaField(){ Name="adImage", DisplayName="ad Image", Type=SPFieldType.URL, Required=true },
            new SchemaField(){ Name="adLink", DisplayName="adLink", Type=SPFieldType.URL },

            new SchemaField(){ Name="link", DisplayName="link", Type=SPFieldType.URL, Required=true },
            new SchemaField(){ Name="imgLink", DisplayName="img Link", Type=SPFieldType.URL, Required=true },
            //new SchemaField(){ Name="over_x0020_img_x0020_Link", DisplayName="over img Link", Type=SPFieldType.URL, Required=true },
            new SchemaField(){ Name="hoverImgLink", DisplayName="hoverImgLink", Type=SPFieldType.URL, Required=true },
        };

        ContentTypes = new List<SchemaCT>()
        {
            new SchemaCT(){
                ParentCTID="0x01",
                CTID="0x0100D6DF9CFF478C4C488917A9329A31874F",
                Name="BaseContentType",
                myFields = new List<string>(){ "Order", "isActive" }
            },
            new SchemaCT(){
                ParentCTID="0x0100D6DF9CFF478C4C488917A9329A31874F",
                CTID="0x0100D6DF9CFF478C4C488917A9329A31874F01",
                Name="UpdatesContentType",
                myFields = new List<string>(){ "typeOfUpdate", "titleBoldText" }
            },
            new SchemaCT(){
                ParentCTID="0x0100D6DF9CFF478C4C488917A9329A31874F",
                CTID="0x0100D6DF9CFF478C4C488917A9329A31874F02",
                Name="LongAdsContentType",
                myFields = new List<string>(){ "adImage", "adLink" }
            },
            new SchemaCT(){
                ParentCTID="0x0100D6DF9CFF478C4C488917A9329A31874F",
                CTID="0x0100D6DF9CFF478C4C488917A9329A31874F04",
                Name="quickLinksContentType",
                myFields = new List<string>(){ "link", "imgLink", "hoverImgLink" }
            },
        };
    }


}



