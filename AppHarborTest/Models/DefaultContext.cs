using System.Data.Entity;
using System.Linq;

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

        public DefaultContext()
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultContextHarbor"].ConnectionString)
        {
            if (Steps.Count() == 0) { InitData(); }
        }

        public DbSet<Step> Steps { get; set; }
        public DbSet<Section> Sections { get; set; }

        public void InitData()
        {
            Section[] newSections = new Section[] {
                new Section() { Name = "Εισαγωγή", OrderNum = 1 },
                new Section() { Name = "GitHub Configuration", OrderNum = 2 },
                new Section() { Name = "AppHarbor Configuration", OrderNum = 3 },
                new Section() { Name = "Συγχαρητήρια!", OrderNum = 4 },
            };

            foreach (Section section in newSections)
            {
                Sections.Add(section);
            }

            SaveChanges();
            
            Step[] newSteps = new Step[] {
                new Step() {
                    Name = "Καλώστο!",
                    Description = "Αλεξάκι, απλώς ακολούθησε ευλαβικά τα βήματα και θα έχεις την εργασία σου deployed σε ένα 20λεπτο.",
                    Tip = "Αν δεν το κάνεις για σένα, σκέψου τουλάχιστον τον κόπο μου.",
                    OrderNum = 1,
                    SectionID = this.Sections.Where(o => o.OrderNum == 1).Select(o => o.ID).Single()
                },
                new Step() {
                    Name = "Λογαριασμός GitHub",
                    Description = "Δημιούργησε ένα λογαριασμό στο GitHub",
                    Tip = "<a href=\"https://github.com\">https://github.com</a> αν βαριέσαι και να googlάρεις-ίσεις",
                    OrderNum = 2,
                    SectionID = this.Sections.Where(o => o.OrderNum == 1).Select(o => o.ID).Single()
                },
                new Step() {
                    Name = "GitHub Repo",
                    Description = "Δημιούργησε ένα νέο repository στο GitHub. Μην κλείσεις τη σελίδα, θα χρειαστείς το link του repository που είναι της μορφής git@github.com:tedkx/AppHarborTest.git",
                    Tip = "Αν το ψάχνεις, είναι ένα από τα πάνω δεξιά κουμπάκια. Βάλε ότι όνομα θέλεις, μόνο εσύ θα το βλέπεις.",
                    OrderNum = 3,
                    SectionID = this.Sections.Where(o => o.OrderNum == 1).Select(o => o.ID).Single()
                },
                new Step() {
                    Name = "GitExtensions Download",
                    Description = "Κατέβασε το GitExtensions for Visual Studio 2010",
                    Tip = "Πάρτο από δω <a href=\"http://sourceforge.net/projects/gitextensions/files/latest/download\">http://sourceforge.net/projects/gitextensions/files/latest/download</a>",
                    OrderNum = 4,
                    SectionID = this.Sections.Where(o => o.OrderNum == 1).Select(o => o.ID).Single()
                },
                new Step() {
                    Name = "GitExtensions Install",
                    Description = "Εγκατάστησε το GitExtensions. Βεβαιώσου ότι έχεις τσεκάρει το checkbox που λέει \"Install MsysGit 1.8.3\" ή κάπως έτσι",
                    OrderNum = 5,
                    SectionID = this.Sections.Where(o => o.OrderNum == 1).Select(o => o.ID).Single()
                },
            };

            foreach (Step step in newSteps)
            {
                Steps.Add(step);
            }

            SaveChanges();
        }
    }
}
