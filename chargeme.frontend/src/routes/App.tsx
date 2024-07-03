import React from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import PrivateRoute from './PrivateRoute';
import Dashboard from '../pages/Dashboard';
import Auth from '../pages/Auth';
import Layout from '../components/Layout/Layout';
import Map from '@/pages/Map';
import Vehicle from '@/pages/Vehicle';
import Wallet from '@/pages/Wallet';
import Analytics from '@/pages/Analytics';
import RegistrationConfirm from '@/pages/RegistrationConfirm';

const App: React.FC = () => {
  const isAuthenticated = true;

  return (
    <Routes>
      {/* Rotta di default che reindirizza a /auth */}
      <Route path="/" element={<Navigate to="/auth" />} />

      {/* Rotte pubbliche */}
      <Route path="/auth" element={<Auth />} />
      <Route path="/auth/register-confirm" element={<RegistrationConfirm />} />

      {/* Rotte protette */}
      <Route
        path="/dashboard"
        element={
          <PrivateRoute isAuthenticated={isAuthenticated}>
            <Layout>
              <Dashboard />
            </Layout>
          </PrivateRoute>
        }
      />
      <Route
        path="/map"
        element={
          <PrivateRoute isAuthenticated={isAuthenticated}>
            <Layout>
              <Map />
            </Layout>
          </PrivateRoute>
        }
      />
      <Route
        path="/vehicle"
        element={
          <PrivateRoute isAuthenticated={isAuthenticated}>
            <Layout>
              <Vehicle />
            </Layout>
          </PrivateRoute>
        }
      />
      <Route
        path="/wallet"
        element={
          <PrivateRoute isAuthenticated={isAuthenticated}>
            <Layout>
              <Wallet />
            </Layout>
          </PrivateRoute>
        }
      />
      <Route
        path="/analytics"
        element={
          <PrivateRoute isAuthenticated={isAuthenticated}>
            <Layout>
              <Analytics />
            </Layout>
          </PrivateRoute>
        }
      />
    </Routes>
  );
};

export default App;
