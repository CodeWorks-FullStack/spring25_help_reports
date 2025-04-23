import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Restaurant } from "@/models/Restaurant.js"

class RestaurantsService {
  async getAllRestaurants() {
    const response = await api.get('api/restaurants')
    logger.log('GOT RESTAURANTS', response.data)
    AppState.restaurants = response.data.map(pojo => new Restaurant(pojo))
  }
}

export const restaurantsService = new RestaurantsService()