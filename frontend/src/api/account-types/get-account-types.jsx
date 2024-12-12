import { queryOptions, useQuery } from "@tanstack/react-query";
import { api } from "~/configs/api";

export const getAccountTypes = async ({ page="", size="", keyword=""}) => {
  const response = await api.get(`/account-types`, {
    params: {page, size, keyword},
  });
  return response.data; 
};

export const getAccountTypesQueryOptions = ({ page, size, keyword }) => {
  return queryOptions({
    queryKey: page ? ["account-types", { page, size, keyword }] : ["account-types"],
    queryFn: () => getAccountTypes({ page, size, keyword }),
  });
};
export const useAccountTypes = ({ queryConfig, page, size, keyword }) => {
  return useQuery({
    ...getAccountTypesQueryOptions({ page, size, keyword }),
    ...queryConfig,
  });
};
export const getAllAccountTypes = async () => {
  const response = await api.get(`/account-types`);
  return response.data;
};
