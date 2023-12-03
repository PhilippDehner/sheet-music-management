namespace Backend.Types;

public class SheetMusicCollection
{
	public uint Id { get; set; }
	public uint Version { get; set; }
	public string Name { get; set; }

	public Person? Author { get; set; }
	public string? Publisher { get; set; }

	public bool CompleteRegistered { get; set; }

	public bool AvailableAsFile { get; set; } = false;
	public string? FilePath { get; set; }

	public bool AvailableOnPaper { get; set; } = false;
	public bool? IsSinglePaper { get; set; } = null;

	public SheetMusicCollection? BaseCollection { get; set; }

	public List<SheetMusic> SheetMusic { get; set; } = [];

	public string? Remark { get; set; }

	public SheetMusicCollection()
	{
		Name = string.Empty;
	}
	public SheetMusicCollection(string name)
	{
		Name = name;
	}
}
