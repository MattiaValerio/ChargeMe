// src/App.tsx
import React from 'react';
import { Route, Routes } from 'react-router-dom';
import PrivateRoute from './PrivateRoute';
import ChargingStation from '../pages/ChargingStation';
import Home from '../pages/Home';
import Auth from '@/pages/Auth';
// import Home from './pages/Home';

const App: React.FC = () => {
  const isAuthenticated = false;

  return (
    <Routes>
      {/* Rotte pubbliche */}
      <Route path="/auth" element={<Auth />} />

      {/* Rotte protette */}
      <Route
        path="/"
        element={
          <PrivateRoute isAuthenticated={isAuthenticated}>
            <Home />
          </PrivateRoute>
        }
      />
      <Route
        path="/charging-station"
        element={
          <PrivateRoute isAuthenticated={isAuthenticated}>
            <ChargingStation />
          </PrivateRoute>
        }
      />
    </Routes>
  );
};

export default App;
