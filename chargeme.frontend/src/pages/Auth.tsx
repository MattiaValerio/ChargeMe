import React from 'react';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import Login from '../components/login';
import Register from '../components/Register';

const Auth: React.FC = () => {
  return (
    <div className="w-full h-screen lg:grid lg:grid-cols-2">
      <div className="hidden lg:block bg-secondary  relative">
        <img
          src={'/img/light/logo_transparent.png'}
          alt="Logo"
          className="absolute top-0 right-0 w-[30%] h-[30%]"
        />
        <p className="absolute bottom-4 left-2 right-0 px-8 py-8 text-[34px] italic font-extralight mb-4 text-[#364f6b]">
          "Once you recharge your car at one of our columns you will look
          forward to doing it again"
        </p>
      </div>
      <div className="flex items-center justify-center py-12 lg:px-0 px-4">
        <div className="mx-auto grid w-full max-w-md gap-6">
          <div className="grid gap-4">
            <div className="grid gap-2">
              <div className="flex items-center">
                <Tabs defaultValue="login" className="w-full">
                  <TabsList className="grid w-full grid-cols-2">
                    <TabsTrigger value="login">Login</TabsTrigger>
                    <TabsTrigger value="register">Register</TabsTrigger>
                  </TabsList>
                  <TabsContent value="login">
                    <Login />
                  </TabsContent>
                  <TabsContent value="register">
                    <Register />
                  </TabsContent>
                </Tabs>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Auth;
