import { z, ZodType } from 'zod';

export type LoginFormData = {
  email: string;
  password: string;
  acceptTerms: boolean;
};

export const LoginSchema: ZodType<LoginFormData> = z.object({
  email: z
    .string()
    .min(1, { message: 'This email field has to be filled' })
    .email('This is not a valid email.'),
  password: z
    .string()
    .min(8, { message: 'Password is too short' })
    .max(20, { message: 'Password is too long' })
    .regex(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$/,
      {
        message:
          'The password must contain at least one lowercase letter, one uppercase letter, one number, and one special character',
      }
    )
    .refine((value) => !/\s/.test(value), {
      message: 'The password must not contain any spaces',
    }),
  acceptTerms: z.boolean().refine((value) => value === true, {
    message: 'You must accept the terms and conditions',
  }),
});
