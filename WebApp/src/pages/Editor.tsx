import { Button } from "react-bootstrap";
import Song from "../types/Song";
import { useState } from "react";

function Editor() {
  const [selectedSong, setSelectedSong]= useState<Song | undefined>(undefined);

  const userAction = async () => {
    const response = await fetch('http://localhost:5289/song', {
      method: 'GET',
      // body: myBody, // string or object
      headers: {
        'Content-Type': 'application/json'
      }
    });
    const jsonData = await response.json();
    setSelectedSong(new Song(jsonData[0]) ?? undefined);
    console.debug("selectedSong", selectedSong);
  };

  return <div>
    <h2>Editor</h2>
    <Button onClick={userAction}>Fetch</Button>
    <table>
      <tbody>
        <tr>
          <td>Title</td>
          <td>{selectedSong?.Title}</td>
        </tr>
        <tr>
          <td>Sub title</td>
          <td>{selectedSong?.SubTitle}</td>
        </tr>
      </tbody>
    </table>
  </div>;
}

export default Editor;