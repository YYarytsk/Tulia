import { Group } from "./group";

export interface Membership {
    id: number,
    groupId: number,
    userId: number,
    group: Group
}