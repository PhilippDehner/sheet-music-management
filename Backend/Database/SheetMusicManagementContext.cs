using Backend.Enums;
using Backend.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Npgsql;
using System.Reflection.Emit;

namespace Backend.DbContexts;

public class SheetMusicManagementContext : DbContext
{
	private readonly int _maxTitleLength = 50;
	private readonly int _maxPathLength = 250;
	private readonly int _maxRemarksLength = 500;
	private readonly string _connectionString = string.Empty;

	public DbSet<SheetMusic> SheetMusic => Set<SheetMusic>();
	public DbSet<Song> Songs => Set<Song>();
	public DbSet<SheetMusicCollection> Collections => Set<SheetMusicCollection>();
	public DbSet<Person> People => Set<Person>();

	public SheetMusicManagementContext(DbContextOptions<SheetMusicManagementContext> options) : base(options) { }

	public SheetMusicManagementContext(string connectionString)
	{
		_connectionString = connectionString;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		NpgsqlConnection.GlobalTypeMapper.MapEnum<Instrument>();
		NpgsqlConnection.GlobalTypeMapper.MapEnum<SheetMusicType>();

		//var dataSourceBuilder = new NpgsqlDataSourceBuilder();
		//dataSourceBuilder.MapEnum<Instrument>();
		//dataSourceBuilder.MapEnum<SheetMusicType>();
		//dataSourceBuilder.Build();

		optionsBuilder.UseNpgsql(_connectionString)
#if DEBUG
			.LogTo(Console.WriteLine, LogLevel.Information)
			.EnableSensitiveDataLogging()
#endif
			;

		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasPostgresEnum<Instrument>();
		modelBuilder.HasPostgresEnum<SheetMusicType>();

		var songs = modelBuilder.Entity<Song>();
		songs.HasKey(x => x.Id);
		songs.HasIndex(x => x.Id);
		songs.HasIndex(x => x.Title);
		songs.Property(x => x.Version).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
		songs.Property(x => x.Title).HasMaxLength(_maxTitleLength);
		songs.Property(x => x.SubTitle).HasMaxLength(_maxTitleLength);
		songs.HasOne(x => x.Composer).WithOne().HasForeignKey<Person>(x => x.Id);
		songs.HasOne(x => x.CopyWriter).WithOne().HasForeignKey<Person>(x => x.Id);
		songs.HasOne(x => x.Arranger).WithOne().HasForeignKey<Person>(x => x.Id);
		songs.Property(x => x.Remarks).HasMaxLength(_maxRemarksLength);

		var sheetMusic = modelBuilder.Entity<SheetMusic>();
		sheetMusic.HasKey(x => x.Id);
		sheetMusic.HasIndex(x => x.Id);
		sheetMusic.Property(x => x.Version).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
		sheetMusic.Property(x => x.Remark).HasMaxLength(_maxRemarksLength);
		sheetMusic.HasOne(sheet => sheet.Arranger).WithOne().HasForeignKey<Person>(x => x.Id);
		sheetMusic.Property(x => x.Remark).HasMaxLength(_maxRemarksLength);
		sheetMusic.Property(x => x.FilePath).HasMaxLength(_maxPathLength);
		sheetMusic.HasOne(sheet => sheet.Song).WithMany(song => song.SheetMusic);
		sheetMusic.HasOne(sheet => sheet.SheetMusicCollection).WithMany(collection => collection.SheetMusic);

		var collections = modelBuilder.Entity<SheetMusicCollection>();
		collections.HasKey(x => x.Id);
		collections.HasIndex(x => x.Id);
		collections.HasIndex(x => x.Name);
		collections.Property(x => x.Version).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
		collections.Property(x => x.Name).HasMaxLength(_maxTitleLength);
		collections.HasOne(collection => collection.Author).WithOne().HasForeignKey<Person>(x => x.Id);
		collections.Property(x => x.Publisher).HasMaxLength(_maxTitleLength);
		collections.Property(x => x.Remark).HasMaxLength(_maxRemarksLength);

		var people = modelBuilder.Entity<Person>();
		people.HasKey(x => x.Id);
		people.HasIndex(x => x.Id);
		people.Property(x => x.LastName).HasMaxLength(_maxTitleLength);
		people.Property(x => x.FirstName).HasMaxLength(_maxTitleLength);

		base.OnModelCreating(modelBuilder);
	}
}
