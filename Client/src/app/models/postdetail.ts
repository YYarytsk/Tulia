import { Comment } from "./comment";
export interface PostDetail {
    id: number,
    userId: number,
    title: string,
    body: string,
    groupId: number,
    comments: Comment[],

}