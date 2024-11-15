import { create } from "zustand";

export const useAccountStore = create((set) => ({
  account: null,
  openCreateModal: false,
  openUpdateModal: false,
  openDeleteModal: false,
  setAccount: (account) => set({ account }),
  setOpenCreateModal: (openCreateModal) => set({ openCreateModal }),
  setOpenUpdateModal: (openUpdateModal) => set({ openUpdateModal }),
  setOpenDeleteModal: (openDeleteModal) => set({ openDeleteModal }),
}));
