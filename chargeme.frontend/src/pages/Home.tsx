// src/pages/Home.tsx
import React from 'react';
import MapComponent from '@/components/MapComponent';
import { Navbar } from '@/components/Navbar';

const Home: React.FC = () => {
  return (
    <div className='v-screen h-screen'>
      <MapComponent />
      <Navbar
        name={'altredo'}
        surname={'birilli'}
      />
    </div>
  );
};

export default Home;
