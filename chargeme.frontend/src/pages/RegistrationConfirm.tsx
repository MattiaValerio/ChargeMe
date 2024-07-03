import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Card, CardContent, CardHeader } from '@/components/ui/card';

const RegistrationConfirm: React.FC = () => {
  const navigate = useNavigate();

  useEffect(() => {
    const timer = setTimeout(() => {
      navigate('/auth');
    }, 2500);

    return () => clearTimeout(timer);
  }, [navigate]);

  return (
    <React.Fragment>
      <div className="flex items-center justify-center py-12 lg:px-0 px-4">
        <div className="mx-auto grid w-full max-w-md gap-6">
          <Card>
            <CardHeader>
              <h2 className="text-xl font-semibold">
                Registration Successful 🥳
              </h2>
            </CardHeader>
            <CardContent>
              <p className="text-md">
                Thank you for registering. In order to use our services you must
                login to our platform. You will shortly be redirected to the
                login page.
              </p>
            </CardContent>
          </Card>
        </div>
      </div>
    </React.Fragment>
  );
};

export default RegistrationConfirm;
