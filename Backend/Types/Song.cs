namespace Backend.Types;

public class Song
{
	#region Properties

	public uint Id { get; set; }
	public uint Version { get; set; }

	public string Title { get; set; }
	public string? SubTitle { get; set; }

	public List<string> AlternativeLanguagesTitles { get; set; } = [];
	public List<string> AlternativeTitles { get; set; } = [];

	public Person? Composer { get; set; }
	public Person? CopyWriter { get; set; }
	public Person? Arranger { get; set; }

	public string? Remarks { get; set; }

	public List<SheetMusic> SheetMusic { get; set; }

	#endregion

	#region Constructor

	public Song()
	{
		Title = string.Empty;
		SheetMusic = [];
	}
	public Song(string title)
	{
		Title = title;
		SheetMusic = [];
	}

	#endregion

	#region Methods

	public List<string> AllTitles(bool notOtherLanguages = false)
	{
		var list = new List<string> { Title };
		if (SubTitle != null) list.Add(SubTitle);
		if (!notOtherLanguages) list.AddRange(AlternativeLanguagesTitles);
		list.AddRange(AlternativeTitles);
		return list;
	}

	#endregion
}
