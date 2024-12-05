import { create } from "zustand";

export const useAccountTypesStore = create((set) => ({
  accountType: null,
  openCreateModal: false,
  openUpdateModal: false,
  openDeleteModal: false,
  setAccountType: (accountType) => set({ accountType }),
  setOpenCreateModal: (openCreateModal) => set({ openCreateModal }),
  setOpenUpdateModal: (openUpdateModal) => set({ openUpdateModal }),
  setOpenDeleteModal: (openDeleteModal) => set({ openDeleteModal }),
}));
