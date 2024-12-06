import { useQuery } from "@tanstack/react-query";
import { api } from "~/configs/api";

export const getAccountTypes = async () => {
  const response = await api.get(`/account-types`);
  return response.data; 
};

export const useAccountTypes = () => {
  return useQuery({
    queryKey: ["accountTypes"], 
    queryFn: getAccountTypes, 
    
  });
};
