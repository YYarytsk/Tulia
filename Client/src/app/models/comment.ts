import { Time } from "@angular/common";

export interface Comment {
    id: number,
    userId: number,
    groupId: number,
    content: string,
    time: Time
}