using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinema.Repository.Migrations
{
    public partial class combinedMoviesAndTicketEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Movies_MovieId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ShoppingCarts_ShoppingCartId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_MovieId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ShoppingCartId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TimeBought",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "MovieDescription",
                table: "Tickets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MovieGenre",
                table: "Tickets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MovieImage",
                table: "Tickets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MovieName",
                table: "Tickets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MovieYear",
                table: "Tickets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TicketsInOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TicketId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketsInOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketsInOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketsInOrders_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketsInShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TicketId = table.Column<Guid>(nullable: false),
                    ShoppingCartId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketsInShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketsInShoppingCarts_Tickets_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketsInShoppingCarts_ShoppingCarts_TicketId",
                        column: x => x.TicketId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketsInOrders_OrderId",
                table: "TicketsInOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsInOrders_TicketId",
                table: "TicketsInOrders",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsInShoppingCarts_ShoppingCartId",
                table: "TicketsInShoppingCarts",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsInShoppingCarts_TicketId",
                table: "TicketsInShoppingCarts",
                column: "TicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketsInOrders");

            migrationBuilder.DropTable(
                name: "TicketsInShoppingCarts");

            migrationBuilder.DropColumn(
                name: "MovieDescription",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MovieGenre",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MovieImage",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MovieName",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MovieYear",
                table: "Tickets");

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingCartId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeBought",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieGenre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoviePrice = table.Column<int>(type: "int", nullable: false),
                    MovieYear = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MovieId",
                table: "Tickets",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ShoppingCartId",
                table: "Tickets",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Movies_MovieId",
                table: "Tickets",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ShoppingCarts_ShoppingCartId",
                table: "Tickets",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
