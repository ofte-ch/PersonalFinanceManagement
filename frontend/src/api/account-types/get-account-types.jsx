import { useQuery } from "@tanstack/react-query";
import { api } from "~/configs/api";

export const getAccountTypes = async ({ page="", size="" }) => {
  const response = await api.get(`/account-types`);
  return response.data; 
};

/*
export const getAccountTypesQueryOptions = ({ page, size }) => {
  return queryOptions({
    queryKey: page ? ["account-types", { page, size }] : ["account-types"],
    queryFn: () => getAccountTypes({ page, size }),
  });
};
export const useAccountTypes = ({ queryConfig, page, size }) => {
  return useQuery({
    ...getAccountTypesQueryOptions({ page, size }),
    ...queryConfig,
  });
};
*/

export const useAccountTypes = () => {
  return useQuery({
    queryKey: ["accountTypes"], 
    queryFn: getAccountTypes, 
    
  });
};