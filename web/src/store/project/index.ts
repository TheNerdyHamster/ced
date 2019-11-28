import { ProjectState } from "./types";
import { Module } from "vuex";
import { RootState } from "../types";
import { actions } from "./actions";
import { getters } from "./getters";
import { mutations } from "./mutations";

export const state: ProjectState = {
  project: undefined,
  projects: [],
  error: false
};

const namespaced: boolean = true;

export const project: Module<ProjectState, RootState> = {
  namespaced,
  state,
  actions,
  mutations,
  getters
};
