# campaign-price-calculator-gui
Campaign Price Calculator with GUI
This project is a GUI solution for the task.
#How it works
- It loads a list of products and instantiates a cart
- It also instantiates a volume campaign and a combo campaign
- You can add and remove items from the cart
- When items are added or removed the prices for the campaigns are dynamically calculated depending on what is added.

NOTE: The campaigns are instantiated based on examples provided in the doc. 

#Assumptions made
- A campaign can only be applied to a cart once
- All products cost 100 SEK
- Multiple campaign prices cannot be calculated recursively

#Possible improvements
- Duplication in the cart can be resolved by the CartItem storing a quantity in cart. This can be updated when items are added or removed
- Iterative application of campaigns.
