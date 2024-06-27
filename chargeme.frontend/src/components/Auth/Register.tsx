import React from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { RegisterSchema } from "@/types/RegisterSchema";
import { z } from "zod";
import { Button } from "@/components/ui/button";
import { Label } from "@radix-ui/react-label";
import { Input } from "@/components/ui/input";
import { Card, CardContent, CardFooter } from "@/components/ui/card";

const Register: React.FC = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<z.infer<typeof RegisterSchema>>({
    resolver: zodResolver(RegisterSchema),
    defaultValues: {
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      confirmPassword: "",
      city: "",
      address: "",
    },
  });

  const onSubmit = (values: z.infer<typeof RegisterSchema>) => {
    // create an object with all the values and fetch it to the backend
    const person = {
      firstName: values.firstName,
      lastName: values.lastName,
      email: values.email,
      password: values.password,
      city: values.city,
      address: values.address,
    };

    console.log(JSON.stringify(person));
  };

  return (
    <Card>
      <form onSubmit={handleSubmit(onSubmit)}>
        <CardContent className="space-y-2">
          <div className="grid grid-cols-2 gap-4 mt-4">
            <div className="grid gap-2 ">
              <Label htmlFor="firstName">First name</Label>
              <Input id="firstName" {...register("firstName")} />
              {errors.firstName && (
                <p className="error-message">{errors.firstName.message}</p>
              )}
            </div>
            <div className="grid gap-2">
              <Label htmlFor="lastName">Last name</Label>
              <Input id="lastName" {...register("lastName")} />
              {errors.lastName && (
                <p className="error-message">{errors.lastName.message}</p>
              )}
            </div>
          </div>
          <div className="space-y-1">
            <Label htmlFor="email">E-mail</Label>
            <Input id="email" type="email" {...register("email")} />
            {errors.email && (
              <p className="error-message">{errors.email.message}</p>
            )}
          </div>
          <div className="space-y-1">
            <Label htmlFor="password">Password</Label>
            <Input id="password" type="password" {...register("password")} />
            {errors.password && (
              <p className="error-message">{errors.password.message}</p>
            )}
          </div>
          <div className="space-y-1">
            <Label htmlFor="confirmPassword">Password Confirm</Label>
            <Input
              id="confirmPassword"
              type="password"
              {...register("confirmPassword")}
            />
            {errors.confirmPassword && (
              <p className="error-message">{errors.confirmPassword.message}</p>
            )}
          </div>
          <div className="grid grid-cols-2 gap-4 mt-4">
            <div className="grid gap-2 ">
              <Label htmlFor="city">City</Label>
              <Input id="city" {...register("city")} />
              {errors.city && (
                <p className="error-message">{errors.city.message}</p>
              )}
            </div>
            <div className="grid gap-2">
              <Label htmlFor="address">Address</Label>
              <Input id="address" {...register("address")} />
              {errors.address && (
                <p className="error-message">{errors.address.message}</p>
              )}
            </div>
          </div>
        </CardContent>
        <CardFooter>
          <Button type="submit">Register</Button>
        </CardFooter>
      </form>
    </Card>
  );
};

export default Register;
