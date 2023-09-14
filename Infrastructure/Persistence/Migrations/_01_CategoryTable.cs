using FluentMigrator;

namespace Marketplace.Persistence.Migrations;

[Migration(2023052301)]
public class _01_CategoryTable :Migration
{
    public override void Up()
    {
        Create.Table("Category")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
            .WithColumn("Name").AsString(50).NotNullable();

        Insert.IntoTable("Category").Row(new { Name = "Cars" });
        Insert.IntoTable("Category").Row(new { Name = "Bike" });
        Insert.IntoTable("Category").Row(new { Name = "Scooter" });
        Insert.IntoTable("Category").Row(new { Name = "Boat" });
        Insert.IntoTable("Category").Row(new { Name = "Commercial properties" });
        Insert.IntoTable("Category").Row(new { Name = "Construction equipment" });
    }

    public override void Down()
    {
        Delete.Table("Category");
    }
}