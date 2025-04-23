import { DatabaseItem } from "./DatabaseItem.js"
export class Profile extends DatabaseItem {
  constructor(data) {
    super(data)
    this.name = data.name
    this.picture = data.picture
  }
}
export class Account extends Profile {
  /**
   * @typedef AccountData
   * @property {string} id
   * @property {string} email
   * @property {string} name
   * @property {string} picture
   * 
   * @param {AccountData} data
   */
  constructor(data) {
    super(data)
    this.email = data.email
    // TODO add additional properties if needed
  }
}


