/* eslint-disable no-useless-catch */
import axiosInstance from '../axiosInstance';

export const fetchUser = async (userId: string) => {
  try {
    const response = await axiosInstance.get(`/users/${userId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const updateUser = async (userId: string, userData: any) => {
  try {
    const response = await axiosInstance.put(`/users/${userId}`, userData);
    return response.data;
  } catch (error) {
    throw error;
  }
};
