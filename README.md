{
    "descripcion general": "El proyecto consiste en el desarrollo de un backend en ASP.NET Core Web API 9.0 dividido en tres capas:.Api, .Core, .Infrastructure",
    "objetivo": "Su objetivo es gestionar un sistema basado en un modelo de inteligencia artificial capaz de analizar textos y generar predicciones mediante ML.NET.",
    "capas"
    {
        "api":"expone los endpoints REST y utiliza AutoMapper y FluentValidation para controlar la comunicación con el cliente y validar los datos de entrada",
        "core":"implementa la lógica de negocio, las entidades (User, Text, Prediction), los servicios, las interfaces y el módulo de IA (ModeloIAService), encargado de procesar los textos y generar predicciones",
        "infrastructure":"maneja la persistencia con Entity Framework Core y SQL Server, utilizando un repositorio genérico para las operaciones CRUD"
    },
    "logica de negocio":"El sistema permite registrar usuarios, almacenar textos, analizarlos mediante el modelo de IA y guardar las predicciones resultantes en la base de datos. La estructura sigue una arquitectura
                        limpia y escalable, donde un usuario puede tener múltiples textos y cada texto varias predicciones. El modelo de ML.NET procesa las entradas y devuelve una categoría y un nivel de confianza."
}
