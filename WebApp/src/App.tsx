import './App.css';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Layout from './components/Layout';
import Home from './pages/Home';
import Editor from './pages/Editor';


function App() {

  return (
    <Router>

      <div>
        <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="/" element={<Home />} />
            <Route path="/editor" element={<Editor />} />
            </Route>
        </Routes>
      </div>
    </Router>
  );
}

export default App;
