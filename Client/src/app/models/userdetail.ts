import { Group } from "./group";
import { Membership } from "./membership";

export interface UserDetail {
    id: number,
    firstName: string,
    lastName: string,
    username: string,
    password: string,
    role: string,
    numberGroups: number,
    memberships: Membership[],
    groups: Group[]


}