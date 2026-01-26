import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Games from './pages/Games';
import Add from './pages/Add';
import Update from './pages/Update';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Games />} />
          <Route path="/add" element={<Add />} />
          <Route path="/update" element={<Update />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
