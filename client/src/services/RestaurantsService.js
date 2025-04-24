import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Restaurant } from "@/models/Restaurant.js"

class RestaurantsService {

  async getRestaurantById(restaurantId) {
    AppState.activeRestaurant = null
    const response = await api.get(`api/restaurants/${restaurantId}`)
    logger.log('GOT RESTAURANT', response.data)
    AppState.activeRestaurant = new Restaurant(response.data)
  }
  async getAllRestaurants() {
    const response = await api.get('api/restaurants')
    logger.log('GOT RESTAURANTS', response.data)
    AppState.restaurants = response.data.map(pojo => new Restaurant(pojo))
  }

  async deleteRestaurant(restaurantId) {
    const response = await api.delete(`api/restaurants/${restaurantId}`)
    logger.log('DELETED RESTAURANT', response.data)
    AppState.activeRestaurant = null
  }

  async updateRestaurant(restaurantId, updateData) {
    const response = await api.put(`api/restaurants/${restaurantId}`, updateData)
    logger.log('UPDATED RESTAURANT', response.data)
    AppState.activeRestaurant = new Restaurant(response.data)
  }
}
export const restaurantsService = new RestaurantsService()