namespace TiendaOnlineMvc.Migrations.TiendaOnline
{
    using DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TiendaOnlineMvc.DAL.TiendaOnlineMvcContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\TiendaOnline";
        }

        protected override void Seed(TiendaOnlineMvc.DAL.TiendaOnlineMvcContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Categories.AddOrUpdate(
                t => t.Id, ProductoPrueba.GetCategory().ToArray());
            context.SaveChanges();

            context.Products.AddOrUpdate(
                p => new { p.Name, p.Description }, ProductoPrueba.GetProducts(context).ToArray());

        }
    }

public partial class MetodoEntitiesError : DbContext
{
    public override int SaveChanges()
    {
        try
        {
            return base.SaveChanges();
        }
        catch (DbEntityValidationException ex)
        {
            // Retrieve the error messages as a list of strings.
            var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            // Join the list to a single string.
            var fullErrorMessage = string.Join("; ", errorMessages);

            // Combine the original exception message with the new one.
            var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

            // Throw a new DbEntityValidationException with the improved exception message.
            throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
        }
    }
}

}