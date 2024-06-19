import { ZodType, z } from 'zod';

export type RegisterFormData = {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string;
  city?: string;
  address?: string;
};

export const RegisterSchema: ZodType<RegisterFormData> = z
  .object({
    firstName: z.string().nonempty('Inserisci nome'),
    lastName: z.string().nonempty('Inserisci cognome'),
    email: z.string().email('Inserisci un indirizzo email valido'),
    password: z
      .string()
      .min(8, 'La password deve avere almeno 8 caratteri')
      .regex(
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/,
        'La password deve avere almeno una lettera maiuscola, una lettera minuscola, un numero e un carattere speciale'
      ),
    confirmPassword: z.string().min(8, 'Inserisci la conferma della password'),
    company: z.string().optional(),
    city: z.string().optional(),
    address: z.string().optional(),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: 'Le password devono corrispondere',
    path: ['confirmPassword'],
  });
