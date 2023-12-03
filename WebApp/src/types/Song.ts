interface jsonSong {
  id: number;
  title: string;
  subTitle?: string;
  alternativeLanguagesTitles: string[];
  alternativeTitles: string[];
}

class Song {
  public Id: number = 0;

  public Title: string;

  SubTitle?: string;

  AlternativeLanguagesTitles: string[] = [];

  AlternativeTitles: string[] = [];

  constructor(title: string);
  constructor(json: jsonSong);
  constructor(arg1: string | jsonSong) {
    if (typeof arg1 === 'string') {
      this.Title = arg1;
    } else {
      // Constructor with JSON object parameter
      this.Title = arg1.title;
      this.Id = arg1.id;
    }
  }
}

export default Song;