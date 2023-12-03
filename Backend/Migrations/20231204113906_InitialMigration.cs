using System.Collections.Generic;
using Backend.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:instrument", "undefined,melody,soprano,soprano2,alto,alto2,tenor,tenor2,bass,bass2,organ,piano,accordion,flute")
                .Annotation("Npgsql:Enum:sheet_music_type", "undefined,main_piece,improvisation,prelude");

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Publisher = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CompleteRegistered = table.Column<bool>(type: "boolean", nullable: false),
                    AvailableAsFile = table.Column<bool>(type: "boolean", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    AvailableOnPaper = table.Column<bool>(type: "boolean", nullable: false),
                    IsSinglePaper = table.Column<bool>(type: "boolean", nullable: true),
                    BaseCollectionId = table.Column<long>(type: "bigint", nullable: true),
                    Remark = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collections_Collections_BaseCollectionId",
                        column: x => x.BaseCollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Collections_Id",
                        column: x => x.Id,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubTitle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    AlternativeLanguagesTitles = table.Column<List<string>>(type: "text[]", nullable: false),
                    AlternativeTitles = table.Column<List<string>>(type: "text[]", nullable: false),
                    ComposerId = table.Column<long>(type: "bigint", nullable: true),
                    CopyWriterId = table.Column<long>(type: "bigint", nullable: true),
                    Remarks = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_People_ComposerId",
                        column: x => x.ComposerId,
                        principalTable: "People",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Songs_People_CopyWriterId",
                        column: x => x.CopyWriterId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SheetMusic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    SongId = table.Column<long>(type: "bigint", nullable: false),
                    Remark = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AvailableInstruments = table.Column<List<Instrument>>(type: "instrument[]", nullable: false),
                    ChordsSpecified = table.Column<bool>(type: "boolean", nullable: false),
                    SheetMusicTypes = table.Column<List<SheetMusicType>>(type: "sheet_music_type[]", nullable: false),
                    SheetMusicCollectionId = table.Column<long>(type: "bigint", nullable: false),
                    PageInCollection = table.Column<long>(type: "bigint", nullable: true),
                    NumberInCollection = table.Column<long>(type: "bigint", nullable: true),
                    FilePath = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    NumberOfPages = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetMusic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheetMusic_Collections_SheetMusicCollectionId",
                        column: x => x.SheetMusicCollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SheetMusic_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collections_BaseCollectionId",
                table: "Collections",
                column: "BaseCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_Id",
                table: "Collections",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_Name",
                table: "Collections",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_People_Id",
                table: "People",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SheetMusic_Id",
                table: "SheetMusic",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SheetMusic_SheetMusicCollectionId",
                table: "SheetMusic",
                column: "SheetMusicCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SheetMusic_SongId",
                table: "SheetMusic",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ComposerId",
                table: "Songs",
                column: "ComposerId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_CopyWriterId",
                table: "Songs",
                column: "CopyWriterId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_Id",
                table: "Songs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_Title",
                table: "Songs",
                column: "Title");

            migrationBuilder.AddForeignKey(
                name: "FK_People_SheetMusic_Id",
                table: "People",
                column: "Id",
                principalTable: "SheetMusic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Songs_Id",
                table: "People",
                column: "Id",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Collections_Id",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_SheetMusic_Collections_SheetMusicCollectionId",
                table: "SheetMusic");

            migrationBuilder.DropForeignKey(
                name: "FK_People_SheetMusic_Id",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Songs_Id",
                table: "People");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "SheetMusic");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
