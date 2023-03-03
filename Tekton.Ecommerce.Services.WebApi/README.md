<img src="https://github.com/RickStrahl/MarkdownMonster/raw/master/Art/MarkdownMonster_Icon_128.png" align="right" style="height: 64px"/>

# Test Tekton
Presentamos la creación de un Rest API en NET Core 6.0. Aquí su descripción:

* Se adjunta al proyecto el archivo base de datos.sql, el cual contiene la creación de tabla, procedimientos almecenados y registro de datos correspondientes.

* La documentacion de la API se realizó usando swagger.

* Los patrones de diseño aplicados son el Repository y Unit of Work.

* La solución se divide en las siguientes capas:
    * Aplicación: Se definen los objetos de transferencia de datos, los metodos de aplicación e implementación de interfaces.
    * Dominio: Se definen los métodos y lógica del negocio
    * Infraestructura: Responsable de la conexión con los sistemas.
    * Servicio: Aplicaciones específicas del producto.
    * Transversal: Capa común para todas las capas.
   
* Se aplicaron los principios SOLID y Clean Code.

* Para las pruebas unitarias se aplicó MSTest.

* Las validaciones se implementaron utilizando FluentValidation.

* Se recurrió al uso del MemoryCache para mantener en cache(5 min) el diccionario de estados del producto.

* Cada request generado en el día se almacena en un archivo de texto plano.

* Se emplea el uso del MockApi (servicio externo) para aplicar la funcionalidad del descuento de un producto. (https://63f774dee40e087c958f494d.mockapi.io/product/2)


![Image and Preview Themes on the toolbar](https://static.javatpoint.com/tutorial/webapi/images/web-api-tutorial.png) 

