import { useQuery } from "@tanstack/react-query";
import { api } from "~/configs/api";

export const getTypes = async () => {
  const response = await api.get(`/types`);
  return response; 
};

export const getTypesQueryOptions = () => {
  return {
    queryKey: ["types"], 
    queryFn: getTypes, 
  };
};

export const useTypes = ({ queryConfig = {} } = {}) => {
  return useQuery({
    ...getTypesQueryOptions(), 
    ...queryConfig, 
  });
};
