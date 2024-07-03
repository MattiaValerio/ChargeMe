import React from 'react';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Label } from '@/components/ui/label';

interface VehicleData {
  id: number;
  brand: string;
  model: string;
  year: string;
  licensePlate: string;
  discriminator: string;
}

const Vehicle: React.FC = () => {
  const VehicleFakeData: VehicleData = {
    id: 1,
    brand: 'Tesla',
    model: 'Model S',
    year: '2022',
    licensePlate: 'ABC123',
    discriminator: 'Electric',
  };

  const imageUrl = false;

  return (
    <div className="max-w-4xl mx-auto my-8 p-4">
      <Card>
        <CardHeader>
          <CardTitle>Vehicle Information</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          {imageUrl && (
            <img
              src={imageUrl}
              alt={`${VehicleFakeData.brand} ${VehicleFakeData.model}`}
              className="w-full h-64 object-cover rounded-lg"
            />
          )}
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <Label>Brand</Label>
              <p>{VehicleFakeData?.brand}</p>
            </div>
            <div>
              <Label>Model</Label>
              <p>{VehicleFakeData?.model}</p>
            </div>
            <div>
              <Label>Year</Label>
              <p>{VehicleFakeData?.year}</p>
            </div>
            <div>
              <Label>License Plate</Label>
              <p>{VehicleFakeData?.licensePlate}</p>
            </div>
            <div>
              <Label>Type</Label>
              <p>{VehicleFakeData?.discriminator}</p>
            </div>
          </div>
        </CardContent>
      </Card>
    </div>
  );
};

export default Vehicle;
