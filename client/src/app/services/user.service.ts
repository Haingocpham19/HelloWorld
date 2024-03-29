import {
  IAddUserRequest,
  IUpdateUserRequest,
  IUser,
  IPagination,
} from '../store/users/types';
import { api } from '../helpers';

const getCurrentUser = async (): Promise<IUser> => {
  return await api.get<IUser>('/v1/auth').then(response => response.data);
};

const logout = () => {
  sessionStorage.removeItem('user');
};

const getUsers = async (keyword: string, page: number): Promise<IPagination<IUser>> => {
  return await api.get<IPagination<IUser>>(`/v1/users/paging/${page}?keyword=${keyword}`).then(response => response.data);
};

const addUser = async (user: IAddUserRequest): Promise<IUser> => {
  return await api.post<IUser>('/v1/users', user).then(response => response.data);
};

const updateUser = async (id: string, user: IUpdateUserRequest): Promise<IUser> => {
  return await api.put<IUser>(`/v1/users/${id}`, user).then(response => response.data);
};

const getUser = async (id: string): Promise<IUser> => {
  return await api.get<IUser>(`/v1/users/${id}`).then(response => response.data);
};

const deleteUsers = async (ids: string[]): Promise<any> => {
  return await api.delete('/v1/users', { data: ids }).then(response => response.data);
};

export const userService = {
  getCurrentUser,
  logout,
  getUsers,
  addUser,
  updateUser,
  getUser,
  deleteUsers,
};

