import product_data from "@/data/product-data";

export function getMaxPrice () {
  const maxProductPrice = [...product_data].reduce((max, product) => {
    return product.price > max ? product.price : max;
  }, 0);
  return maxProductPrice;
}