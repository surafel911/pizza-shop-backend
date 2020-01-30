''piza-shop-webservice''

Models

The `Customer` class has all the required variables and data attributes for
front-end validation and EF models. You should use this class when
handling Customer CRUD operations. Same thing goes for `Transaction`.
It has all the required variables and data attributes for front-end
validation and EF models.

The use of the `Pizza` and `Drink` classes should be straight forward.
The `Order` class, however, is an object dedicated to handling the
ordering process. Menu items are added into this class. It's intended
to be used regardless of how the front-end is implemented. The 
front-end can use one long page, or several pages. Either way, an
instance of `Order` can be passed around and be manipulated
as needed. At the end of the ordering process when the customer finalizes
the transaction, the Order class will be serialized to a JSON string and
saved in the `OrderJson` attribute in the `Transaction` class to be
stored in the database.
