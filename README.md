# Skills Lab: Hangfire Continuations
Very small skills lab exploring Hangfire job continuations

## 🚧👷‍♂️ Build & Run

This is a _very_ small project and requires no configuration.

Simply `Clone > Restore > Build > Run`

Once running, you can clone the Swagger UI tab and replace the URL path to `/hangfire` to see the Hangfire dashboard.

Using the Swagger UI, expand the `enqueue` endpoint and use the "Try it out" feature to execute the endpoint.

## ✅ Output

In the Hangfire dashboard, you will notice jobs execute in order `JobApple` > `JobBanana` > `JobKiwi`

In the terminal / console that is running with the application, you will notice logs for the sequence of job execution:
```
Added Job: apple with ID 527906d3-d77c-48fb-949b-eb89ea8b5bae
Begin Job: apple
Added Job: banana with ID df145ff8-df2c-4933-970e-faadb53a5396
Added Job: kiwi with ID 8d1e7703-b9a1-4ab8-ae8a-cc28de5e0a60
End Job: apple
Begin Job: banana
End Job: banana
Begin Job: kiwi
```