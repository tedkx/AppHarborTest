using System.Data.Entity;
using System.Linq;
using System.Xml;
using System.IO;

namespace AppHarborTest.Models
{
    public class DefaultContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<AppHarborTest.Models.DefaultContext>());

        public DefaultContext() : base(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultContext"].ConnectionString)
        {
            InitData();
        }

        public DbSet<Step> Steps { get; set; }
        public DbSet<Section> Sections { get; set; }

        public void InitData()
        {
            string dataXmlFile = "AppHarborTest.Data.xml";
            XmlDocument doc = new XmlDocument();
            using (StreamReader reader = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(dataXmlFile)))
            {
                doc.Load(reader);
            }

            XmlNodeList sectionNodes = doc.SelectNodes("Data/Sections/Section");
            XmlNodeList stepNodes = doc.SelectNodes("Data/Steps/Step");
            if (sectionNodes.Count != Sections.Count())
            {
                Database.ExecuteSqlCommand("delete from Sections");
                Database.ExecuteSqlCommand("delete from Steps");

                foreach (XmlNode node in sectionNodes)
                {
                    Sections.Add(new Section()
                    {
                        Name = node.SelectSingleNode("Name").InnerText,
                        OrderNum = int.Parse(node.SelectSingleNode("OrderNum").InnerText),
                    });
                }

                SaveChanges();
            } else if (stepNodes.Count != Steps.Count())
            {
                Database.ExecuteSqlCommand("delete from Steps");
            } 
            else 
            {
                return;
            }

            foreach (XmlNode node in stepNodes)
            {
                int ord = int.Parse(node.SelectSingleNode("SectionOrderNum").InnerText);
                Steps.Add(new Step()
                {
                    Name = node.SelectSingleNode("Name").InnerText,
                    Description = node.SelectSingleNode("Description").InnerText,
                    Tip = node.SelectSingleNode("Tip").InnerText,
                    OrderNum = int.Parse(node.SelectSingleNode("OrderNum").InnerText),
                    SectionID = Sections.Where(s => s.OrderNum == ord).Select(s => s.ID).Single()
                });
            }

            SaveChanges();
        }
    }
}
