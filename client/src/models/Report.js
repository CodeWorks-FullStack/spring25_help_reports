import { Profile } from "./Account.js";
import { DatabaseItem } from "./DatabaseItem.js";

export class Report extends DatabaseItem {
  constructor(data) {
    super(data)
    this.title = data.title
    this.body = data.body ?? ''
    this.score = data.score
    this.imgUrl = data.imgUrl ?? ''
    this.creatorId = data.creatorId
    this.restaurantId = data.restaurantId
    this.reporter = new Profile(data.reporter)
  }
}