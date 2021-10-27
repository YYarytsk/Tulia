import { PostDetail } from "./postdetail";

export interface Group {
    id: number,
    userId: number,
    numberMember: number,
    groupTitle: string,
    description: string,
    posts: PostDetail[],
}