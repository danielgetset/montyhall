<template>
    <div class="container">
        <div class=" columns is-multiline m-5">
            <h1 class="title column is-full">Monty Hall Simulation</h1>

            <b-field class="column is-full" label="Number of simulations">
                <b-input type="number" v-model="params.simulations"></b-input>
            </b-field>

            <b-field class="column is-full" label="Door switch">
                <b-radio-button v-model="params.doorSwitch"
                    name="name"
                    :native-value="true">
                    Yes
                </b-radio-button>
                <b-radio-button v-model="params.doorSwitch"
                    name="name"
                    :native-value="false">
                    No
                </b-radio-button>
            </b-field>

            <div class="column is-full">
                <b-button label="Calculate" type="is-primary" @click="calculate"></b-button>
            </div>

            <!-- <b-loading :is-full-page="true" v-model="loading"></b-loading> -->

            <div class="column is-full">
                Success rate: {{ successRate }}%
                <div v-if="params.doorSwitch">Thank you for the extra 33.3% :D:D</div>
                <img v-if="params.doorSwitch" src="./../assets/thankyou.gif" alt="">
            </div>

            <b-table
                :data="simulation"
                :loading="loading"
                paginated
                :per-page="perPage"
                :current-page.sync="currentPage"
                class="column is-full"
                >

                <b-table-column field="door1" label="Door one" v-slot="{ row }">
                    {{ row.door1 }}
                </b-table-column>
                <b-table-column field="door2" label="Door two" v-slot="{ row }">
                    {{ row.door2 }}
                </b-table-column>
                <b-table-column field="door3" label="Door three" v-slot="{ row }">
                    {{ row.door3 }}
                </b-table-column>
                <b-table-column field="chosenDoor" label="Chosen door" v-slot="{ row }">
                    {{ row.chosenDoor + 1 }}
                </b-table-column>
                <b-table-column field="openDoor" label="Open door" v-slot="{ row }">
                    {{ row.openDoor + 1 }}
                </b-table-column>
                <b-table-column field="switchedDoor" label="Switched door" v-slot="{ row }">
                    {{ row.switchedDoor + 1 }}
                </b-table-column>
                <b-table-column field="success" label="Success" v-slot="{ row }">
                    {{ row.success }}
                </b-table-column>

                <template #empty>
                    <div class="has-text-centered">No records</div>
                </template>

            </b-table>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';

    type Simulation = {
        chosenDoor: number;
        door1: string;
        door2: string;
        door3: string;
        openDoor: number;
        success: boolean
        switchedDoor: number;
    };

    type Params = {
        simulations: number;
        doorSwitch: boolean | string;
    };

    type Forecasts = {
        date: string
    }[];

    interface Data {
        loading: boolean,
        post: null | Forecasts,
        simulation: Simulation[],
        params: Params,
        perPage: number,
        currentPage: number,
        successRate: number,
        successRateSwitched: number
    }

    export default Vue.extend({
        data(): Data {
            return {
                loading: true,
                post: null,
                simulation: [],
                params: { simulations: 100, doorSwitch: true },
                perPage: 15,
                currentPage: 1,
                successRate: 0,
                successRateSwitched: 0
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData(): void {
                this.post = null;
                this.loading = true;

                fetch('weatherforecast?' + "simulations=" + this.params.simulations + "&doorSwitch=" + this.params.doorSwitch)
                    .then(r => r.json())
                    .then(json => {
                        this.simulation = json as Simulation[];
                        this.calculateSuccess();
                        this.loading = false;
                        return;
                    });
            },
            calculate() {
                this.fetchData();
            },
            calculateSuccess() {
                this.successRate = (this.simulation.filter(x => x.success).length / this.simulation.length) * 100
            }
        },
    });
</script>