﻿// <auto-generated />
using System;
using Cemiyet.Persistence.Application.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Cemiyet.Persistence.Application.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20191204204824_AddOrderToSeriesBooks")]
    partial class AddOrderToSeriesBooks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Cemiyet.Core.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Bio")
                        .HasColumnName("bio")
                        .HasColumnType("character varying(2000)")
                        .HasMaxLength(2000);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("creation_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatorId")
                        .HasColumnName("creator_id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(25)")
                        .HasMaxLength(25);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnName("surname")
                        .HasColumnType("character varying(25)")
                        .HasMaxLength(25);

                    b.HasKey("Id")
                        .HasName("pk_authors");

                    b.ToTable("authors");
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.AuthorsBooks", b =>
                {
                    b.Property<Guid>("AuthorsId")
                        .HasColumnName("authors_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BooksId")
                        .HasColumnName("books_id")
                        .HasColumnType("uuid");

                    b.HasKey("AuthorsId", "BooksId")
                        .HasName("pk_authors_books");

                    b.HasIndex("BooksId")
                        .HasName("ix_authors_books_books_id");

                    b.ToTable("authors_books");
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("creation_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatorId")
                        .HasColumnName("creator_id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("character varying(2500)")
                        .HasMaxLength(2500);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.ToTable("books");
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.BookEdition", b =>
                {
                    b.Property<string>("Isbn")
                        .HasColumnName("isbn")
                        .HasColumnType("character varying(13)")
                        .HasMaxLength(13);

                    b.Property<Guid>("BooksId")
                        .HasColumnName("books_id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("creation_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatorId")
                        .HasColumnName("creator_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DimensionsId")
                        .HasColumnName("dimensions_id")
                        .HasColumnType("uuid");

                    b.Property<short>("PageCount")
                        .HasColumnName("page_count")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("PrintDate")
                        .HasColumnName("print_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("PublishersId")
                        .HasColumnName("publishers_id")
                        .HasColumnType("uuid");

                    b.HasKey("Isbn")
                        .HasName("pk_book_editions");

                    b.HasIndex("BooksId")
                        .HasName("ix_book_editions_books_id");

                    b.HasIndex("DimensionsId")
                        .HasName("ix_book_editions_dimensions_id");

                    b.HasIndex("PublishersId")
                        .HasName("ix_book_editions_publishers_id");

                    b.ToTable("book_editions");
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.BooksGenres", b =>
                {
                    b.Property<Guid>("BooksId")
                        .HasColumnName("books_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GenresId")
                        .HasColumnName("genres_id")
                        .HasColumnType("uuid");

                    b.HasKey("BooksId", "GenresId")
                        .HasName("pk_books_genres");

                    b.HasIndex("GenresId")
                        .HasName("ix_books_genres_genres_id");

                    b.ToTable("books_genres");
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.Dimension", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("creation_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatorId")
                        .HasColumnName("creator_id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Height")
                        .HasColumnName("height")
                        .HasColumnType("numeric(4,2)");

                    b.Property<decimal>("Width")
                        .HasColumnName("width")
                        .HasColumnType("numeric(4,2)");

                    b.HasKey("Id")
                        .HasName("pk_dimensions");

                    b.ToTable("dimensions");
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.EntityChange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityId")
                        .HasColumnName("entity_id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnName("modification_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifierId")
                        .HasColumnName("modifier_id")
                        .HasColumnType("uuid");

                    b.Property<string>("NewValue")
                        .HasColumnName("new_value")
                        .HasColumnType("text");

                    b.Property<string>("OldValue")
                        .HasColumnName("old_value")
                        .HasColumnType("text");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnName("property_name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_entity_changes");

                    b.HasIndex("EntityId")
                        .HasName("ix_entity_changes_entity_id");

                    b.ToTable("entity_changes");
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("creation_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatorId")
                        .HasColumnName("creator_id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("pk_genres");

                    b.ToTable("genres");
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.Publisher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("creation_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatorId")
                        .HasColumnName("creator_id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("character varying(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id")
                        .HasName("pk_publishers");

                    b.ToTable("publishers");
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.Serie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("creation_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatorId")
                        .HasColumnName("creator_id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("character varying(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id")
                        .HasName("pk_series");

                    b.ToTable("series");
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.SeriesAuthors", b =>
                {
                    b.Property<Guid>("SeriesId")
                        .HasColumnName("series_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorsId")
                        .HasColumnName("authors_id")
                        .HasColumnType("uuid");

                    b.HasKey("SeriesId", "AuthorsId")
                        .HasName("pk_series_authors");

                    b.HasIndex("AuthorsId")
                        .HasName("ix_series_authors_authors_id");

                    b.ToTable("series_authors");
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.SeriesBooks", b =>
                {
                    b.Property<Guid>("SeriesId")
                        .HasColumnName("series_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BooksId")
                        .HasColumnName("books_id")
                        .HasColumnType("uuid");

                    b.Property<short>("Order")
                        .HasColumnName("order")
                        .HasColumnType("smallint");

                    b.HasKey("SeriesId", "BooksId")
                        .HasName("pk_series_books");

                    b.HasIndex("BooksId")
                        .HasName("ix_series_books_books_id");

                    b.ToTable("series_books");
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.AuthorsBooks", b =>
                {
                    b.HasOne("Cemiyet.Core.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cemiyet.Core.Entities.Book", "Book")
                        .WithMany("Authors")
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.BookEdition", b =>
                {
                    b.HasOne("Cemiyet.Core.Entities.Book", "Book")
                        .WithMany("Editions")
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cemiyet.Core.Entities.Dimension", "Dimensions")
                        .WithMany("BookEditions")
                        .HasForeignKey("DimensionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cemiyet.Core.Entities.Publisher", "Publisher")
                        .WithMany("BookEditions")
                        .HasForeignKey("PublishersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.BooksGenres", b =>
                {
                    b.HasOne("Cemiyet.Core.Entities.Book", "Book")
                        .WithMany("Genres")
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cemiyet.Core.Entities.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.SeriesAuthors", b =>
                {
                    b.HasOne("Cemiyet.Core.Entities.Author", "Author")
                        .WithMany("Series")
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cemiyet.Core.Entities.Serie", "Serie")
                        .WithMany("Authors")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cemiyet.Core.Entities.SeriesBooks", b =>
                {
                    b.HasOne("Cemiyet.Core.Entities.Book", "Book")
                        .WithMany("Series")
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cemiyet.Core.Entities.Serie", "Serie")
                        .WithMany("Books")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
