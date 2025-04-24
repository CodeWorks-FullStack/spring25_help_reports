import { Profile } from "./Account.js";
import { DatabaseItem } from "./DatabaseItem.js";

export class Restaurant extends DatabaseItem {
  constructor(data) {
    super(data)
    this.name = data.name
    this.imgUrl = data.imgUrl
    this.description = data.description
    this.visits = data.visits
    this.isShutdown = data.isShutdown
    this.creatorId = data.creatorId
    this.owner = new Profile(data.owner)
    this.reportCount = data.reportCount
  }
}
