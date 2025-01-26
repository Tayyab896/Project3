namespace myProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuctionReports",
                c => new
                    {
                        ReportID = c.Int(nullable: false, identity: true),
                        AuctionID = c.Int(nullable: false),
                        WinningBid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalSalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ReportID)
                .ForeignKey("dbo.Auctions", t => t.AuctionID, cascadeDelete: true)
                .Index(t => t.AuctionID);
            
            CreateTable(
                "dbo.Auctions",
                c => new
                    {
                        AuctionID = c.Int(nullable: false, identity: true),
                        PropertyID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        MinBidIncrement = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReservePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AuctionID)
                .ForeignKey("dbo.Properties", t => t.PropertyID, cascadeDelete: true)
                .Index(t => t.PropertyID);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        PropertyID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        ImageURL = c.String(maxLength: 255),
                        VideoURL = c.String(maxLength: 255),
                        SellerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyID)
                .ForeignKey("dbo.Users", t => t.SellerID, cascadeDelete: true)
                .Index(t => t.SellerID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        UserType = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        BidID = c.Int(nullable: false, identity: true),
                        AuctionID = c.Int(nullable: false),
                        BuyerID = c.Int(nullable: false),
                        BidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BidDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BidID)
                .ForeignKey("dbo.Auctions", t => t.AuctionID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.BuyerID, cascadeDelete: true)
                .Index(t => t.AuctionID)
                .Index(t => t.BuyerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "BuyerID", "dbo.Users");
            DropForeignKey("dbo.Bids", "AuctionID", "dbo.Auctions");
            DropForeignKey("dbo.AuctionReports", "AuctionID", "dbo.Auctions");
            DropForeignKey("dbo.Auctions", "PropertyID", "dbo.Properties");
            DropForeignKey("dbo.Properties", "SellerID", "dbo.Users");
            DropIndex("dbo.Bids", new[] { "BuyerID" });
            DropIndex("dbo.Bids", new[] { "AuctionID" });
            DropIndex("dbo.Properties", new[] { "SellerID" });
            DropIndex("dbo.Auctions", new[] { "PropertyID" });
            DropIndex("dbo.AuctionReports", new[] { "AuctionID" });
            DropTable("dbo.Bids");
            DropTable("dbo.Users");
            DropTable("dbo.Properties");
            DropTable("dbo.Auctions");
            DropTable("dbo.AuctionReports");
        }
    }
}
