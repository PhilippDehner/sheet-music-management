using Backend.Enums;

namespace Backend.Types;

public class SheetMusic
{
	public uint Id { get; set; }
	public uint Version { get; set; }
	public Song Song { get; set; }
	public string? Remark { get; set; }

	public Person? Arranger { get; set; }

	public List<Instrument> AvailableInstruments { get; set; } = [];
	public bool ChordsSpecified { get; set; }
	public List<SheetMusicType> SheetMusicTypes { get; set; } = [];

	public SheetMusicCollection SheetMusicCollection { get; set; }
	public uint? PageInCollection { get; set; }
	public uint? NumberInCollection { get; set; }
	public string? FilePath { get; set; }

	public uint? NumberOfPages { get; set; }

	public SheetMusic()
	{
		Song = new();
		SheetMusicCollection = new();
	}
	public SheetMusic(Song song, SheetMusicCollection sheetMusicCollection)
	{
		Song = song;
		SheetMusicCollection = sheetMusicCollection;
	}
}
